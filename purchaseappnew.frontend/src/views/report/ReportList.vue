<template>
  <v-container fluid>
    <v-alert v-if="actionTrue" type="success">
      {{ message }}
    </v-alert>
    <h1>Расходы по категориям</h1>
    <v-row dense class="d-flex justify-start">
      <v-col>
        <v-btn @click="showMaximum" color="success" id="maxBtn">
          Наибольшие расходы
        </v-btn>
      </v-col>
      <v-col class="d-flex justify-end">
        <v-btn @click="showMinimum" color="error" id="minBtn">
          Наименьшие расходы
        </v-btn>
      </v-col>
    </v-row>
    <v-simple-table>
      <template v-slot:default>
        <thead>
          <tr>
            <th class="text-left">Категория</th>
            <th class="text-left">Общая стоимость</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="report in reports" :key="report.category.id">
            <td>{{ report.category.categoryName }}</td>
            <td>{{ report.totalPrice }}</td>
          </tr>
        </tbody>
      </template>
    </v-simple-table>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
// Services
import ReportService from "@/services/report-service";
// Types
import { IReport } from "@/types/Report";

const reportService = new ReportService();

@Component({
  name: "ReportList",
  components: {},
})
export default class ReportList extends Vue {
  reports: IReport[] = [];
  message: string = "";
  actionTrue: boolean = false;

  async initialize() {
    this.reports = await reportService.getPurchasesByCategory();
  }

  async created() {
    await this.initialize();
  }

  setSortAlert() {
    this.actionTrue = true;
    this.message = "Отсортировано";
    setTimeout(() => (this.actionTrue = false), 1000);
  }

  async showMaximum() {
    this.reports = await reportService.getMaximumPurchase();
    this.setSortAlert();
  }

  async showMinimum() {
    this.reports = await reportService.getMinimumPurchase();
    this.setSortAlert();
  }
}
</script>