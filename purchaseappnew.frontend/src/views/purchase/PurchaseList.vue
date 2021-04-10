<template>
  <v-container fluid>
    <!-- Уведомление об ошибке -->
    <v-alert v-if="isError" type="error" transition="scroll-y-transition">
      {{ messageError }}
    </v-alert>
    <!-- Навигация -->
    <v-row dense>
      <v-col>
        <v-card>
          <v-card-title>
            <h1>Мои покупки</h1>
            <v-spacer></v-spacer>
            <v-btn
              @click="showNewPurchaseModal"
              color="success"
              id="addNewPurchaseBtn"
              >Добавить покупку
            </v-btn>
          </v-card-title>
          <v-card-text>
            <v-col>
              <v-text-field
                append-icon="search"
                label="Поиск"
                single-line
                hide-details
                v-model="search"
              >
              </v-text-field>
            </v-col>
            <v-spacer></v-spacer>
            <v-col>
              <v-text-field label="С" v-model="startDate"> </v-text-field>
              <v-spacer> </v-spacer>
              <v-text-field label="По" v-model="endDate"></v-text-field>
              <v-btn @click="showPurchasesByDate" color="success">
                Поиск по дате
              </v-btn>
            </v-col>
            <v-col>
              <v-btn @click="exportToExcel" color="primary">
                Экспорт в Excel
              </v-btn>
            </v-col>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <!-- Конец -->
    <!-- Вывод данных -->
    <v-row dense>
      <v-col>
        <v-data-table
          v-model="selected"
          :search="search"
          :headers="headers"
          :items="purchases"
          class="elevation-1"
        >
          <template v-slot:[`item.actions`]="{ item }">
            <v-icon small class="mr-2" @click="showEditModal(item)">
              mdi-pencil
            </v-icon>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
    <!-- Конец -->

    <!-- Добавить новую покупку -->
    <new-purchase-modal
      v-if="isNewPurchaseVisible"
      @save:purchase="saveNewPurchase"
      @close="closeModal"
    />

    <!-- Обновить данные покупки -->
    <edit-purchase-modal
      v-if="isEditPurchaseVisible"
      @edit:purchase="editNewPurchase"
      @close="closeModal"
      :isEditDialogOpen="isEditPurchaseVisible"
      :purchase="selectedPurchase"
    />
    <!-- Конец -->
  </v-container>
</template>

<script lang="ts">
/* eslint-disable */
import { Component, Vue } from "vue-property-decorator";
// Services
import PurchaseService from "@/services/purchase-service";
import TimeChangeService from "@/utils/time-change-service";
import RegexService from "@/utils/regex-service";
import ExcelService from "@/utils/excel-export-service";
// Types
import { IPurchase } from "@/types/Purchase";
// Modal components
import NewPurchaseModal from "@/components/purchase/NewPurchaseModal.vue";
import EditPurchaseModal from "@/components/purchase/EditPurchaseModal.vue";

// Initialization
const purchaseService = new PurchaseService();
const timeChangeService = new TimeChangeService();
const regexService = new RegexService();
const excelService = new ExcelService();

@Component({
  name: "PurchaseList",
  components: { NewPurchaseModal, EditPurchaseModal },
})
export default class PurchaseList extends Vue {
  purchases: IPurchase[] = [];
  selected: [] = [];
  search: string = "";
  selectedPurchase!: IPurchase;
  isNewPurchaseVisible: boolean = false;
  isEditPurchaseVisible: boolean = false;
  startDate: string = "";
  endDate: string = "";
  messageError: string = "";
  isError: boolean = false;
  headers: Object[] = [
    {
      text: "Hазвание продукта",
      align: "start",
      sortable: false,
      value: "product.productName",
    },
    {
      text: "Куплено (количество)",
      value: "quantity",
    },
    {
      text: "Общая стоимость",
      value: "totalPrice",
    },
    {
      text: "Дата покупки",
      value: "buyDate",
    },
    {
      text: "Дата создания покупки",
      value: "dateCreated",
    },
    {
      text: "Действия",
      value: "actions",
      sortable: false,
    },
  ];

  showEditModal(item: IPurchase) {
    this.isEditPurchaseVisible = true;
    this.selectedPurchase = item;
  }

  async initialize() {
    this.purchases = await purchaseService.getPurchasesByUser();
  }

  showNewPurchaseModal() {
    this.isNewPurchaseVisible = true;
  }

  closeModal() {
    this.isNewPurchaseVisible = false;
    this.isEditPurchaseVisible = false;
  }

  async showPurchasesByDate() {
    if (
      !regexService.getRegexForDateSort(this.startDate, this.endDate) ||
      !timeChangeService.compareTwoDates(this.startDate, this.endDate)
    ) {
      this.messageError = "Введите дату";
      this.isError = true;
      setTimeout(() => (this.isError = false), 1000);
      return;
    }
    this.purchases = await purchaseService.getPurchasesByDate(
      this.startDate,
      this.endDate
    );
  }

  async saveNewPurchase(newPurchase: IPurchase) {
    await purchaseService.save(newPurchase);
    this.isNewPurchaseVisible = false;
    await this.initialize();
  }

  async editNewPurchase(selectedPurchase: IPurchase) {
    await purchaseService.update(selectedPurchase);
    this.isEditPurchaseVisible = false;
    await this.initialize();
  }

  async exportToExcel(): Promise<any> {
    let purchases = this.purchases.map((p) => ({
      "Название продукта": p.product.productName,
      Количество: p.quantity,
      "Стоимость:": p.totalPrice,
      "Дата покупки": p.buyDate,
    }));

    let currentTime = timeChangeService.formatDateForExcelFile();
    excelService.exportData(purchases, `my-export ${currentTime}`);
  }

  async created() {
    await this.initialize();
  }
}
</script>
