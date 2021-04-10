<template>
  <!-- Изменить данные покупки -->
  <v-dialog v-model="isDialogOpen" max-width="500px">
    <v-card>
      <v-card-title>Изменить данные покупки</v-card-title>
      <v-card-text>
        <v-container>
          <v-row>
            <v-col>
              <!-- Название продукта -->
              <v-text-field
                label="Hазвание продукта"
                v-model="editedPurchase.product.productName"
                disabled
              ></v-text-field>
              <!--Конец-->
              <!-- Выбор даты покупки -->
              <v-menu v-model="menu">
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    :value="computedDate"
                    clearable
                    label="Дата"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                    @click:clear="editedPurchase.buyDate = null"
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="editedPurchase.buyDate"
                  @change="menu = false"
                ></v-date-picker>
              </v-menu>
              <!-- Конец -->
              <!--Количество продукта -->
              <v-text-field
                label="Количество"
                v-model="editedPurchase.quantity"
              ></v-text-field>
              <!-- Конец -->
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="success"
          @click.native="save"
          type="button"
          aria-label="save edited item"
          >Сохранить</v-btn
        >
        <v-btn color="error" text @click="close" aria-label="close modal">
          Отмена
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <!-- Конец -->
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { IPurchase } from "@/types/Purchase";

@Component({
  name: "EditPurchaseModal",
  components: {},
})
export default class EditPurchaseModal extends Vue {
  @Prop()
  isEditDialogOpen!: boolean;

  @Prop()
  purchase!: IPurchase;

  menu: boolean = false;

  isDialogOpenValue: boolean = false;

  editedPurchase: IPurchase = {
    id: "",
    quantity: "",
    totalPrice: 0,
    productId: "",
    product: {
      id: "",
      productName: "",
      price: 0,
      category: {
        id: "",
        categoryName: "",
      },
    },
    buyDate: "",
    dateCreated: "",
    dateUpdated: "",
  };

  get computedDate() {
    return this.editedPurchase.buyDate;
  }

  get isDialogOpen() {
    return this.isEditDialogOpen ? this.isEditDialogOpen : false;
  }

  set isDialogOpen(newValue) {
    this.isDialogOpenValue = this.isEditDialogOpen;
  }

  mounted() {
    this.editedPurchase = this.purchase;
  }

  close() {
    this.$emit("close");
  }

  save() {
    this.$emit("edit:purchase", this.editedPurchase);
  }
}
</script>
