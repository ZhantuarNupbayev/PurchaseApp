<template>
  <app-modal>
    <template v-slot:header> Добавить новую покупку</template>
    <!-- Validation -->
    <template v-slot:body>
      <validation-observer ref="observer" v-slot="{ invalid }">
        <v-container class="newPurchase">
          <form @submit.prevent="submit">
            <div v-if="!product">
              <!-- Category List -->
              <validation-provider
                v-slot="{ errors }"
                name="Категория"
                rules="required"
              >
                <v-select
                  :items="categories"
                  @change="selectProducts"
                  v-model="selected"
                  label="Категория"
                  item-text="categoryName"
                  item-value="id"
                  id="category-select"
                  :error-messages="errors"
                  dense
                  outlined
                  required
                >
                </v-select>
              </validation-provider>
              <!-- End -->
              <!-- Product List -->
              <validation-provider
                v-slot="{ errors }"
                name="Продукт"
                rules="required"
              >
                <v-select
                  :items="products"
                  item-value="id"
                  item-text="productName"
                  v-model="newPurchase.productId"
                  label="Продукт"
                  :error-messages="errors"
                  dense
                  outlined
                  required
                >
                </v-select>
              </validation-provider>
              <!-- End -->
            </div>
            <div v-else>
              <v-text-field disabled v-model="selected.categoryName">
              </v-text-field>
              <v-text-field disabled v-model="newPurchase.product.productName">
              </v-text-field>
            </div>
            <!-- Purchase Date -->
            <v-menu v-model="menu">
              <template v-slot:activator="{ on, attrs }">
                <validation-provider
                  v-slot="{ errors }"
                  name="Дата покупки"
                  rules="required"
                >
                  <v-text-field
                    :value="computedDate"
                    clearable
                    label="Дата покупки"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                    :error-messages="errors"
                    @click:clear="newPurchase.buyDate = null"
                  ></v-text-field>
                </validation-provider>
              </template>
              <v-date-picker
                v-model="newPurchase.buyDate"
                @change="menu = false"
              ></v-date-picker>
            </v-menu>
            <!-- End -->
            <!-- Quantity -->
            <validation-provider
              v-slot="{ errors }"
              name="Количество"
              rules="required"
            >
              <v-text-field
                id="quantity"
                label="Количество"
                v-model="newPurchase.quantity"
                :error-messages="errors"
              >
              </v-text-field>
            </validation-provider>
            <!-- End -->
            <!-- Button -->
            <v-row>
              <v-col align="left">
                <v-btn
                  type="button"
                  @click.native="save"
                  :disabled="invalid"
                  color="success"
                  aria-label="save new item"
                >
                  Добавить покупку
                </v-btn>
              </v-col>
              <v-col align="right">
                <v-btn color="error" @click="close" aria-label="close modal">
                  Закрыть
                </v-btn>
              </v-col>
            </v-row>
            <!-- End -->
          </form>
        </v-container>
      </validation-observer>
    </template>
    <template v-slot:footer> </template>
  </app-modal>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
// Types
import { IPurchase } from "@/types/Purchase";
import { IProduct } from "@/types/Product";
import { ICategory } from "@/types/Category";
// Validation
import Validation from "@/utils/validation-rule-service";
import PurchaseRules from "@/rules/PurchaseRules";
// Modals
import AppModal from "@/components/modal/AppModal.vue";
// Services
import ProductService from "@/services/product-service";
import CategoryService from "@/services/category-service";
import TimeChangeService from "@/utils/time-change-service";

// Initialization
const productService = new ProductService();
const categoryService = new CategoryService();
const timeChangeService = new TimeChangeService();
const validation = new Validation();
const rules = new PurchaseRules();

// Setting validation rules
validation.setRules(rules.getPurchaseRules());

@Component({
  name: "NewPurchaseModal",
  components: { AppModal },
})
export default class NewPurchaseModal extends Vue {
  @Prop()
  product!: IProduct;
  newPurchase: IPurchase = {
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
  products: IProduct[] = [];
  categories: ICategory[] = [];
  selected: ICategory = {
    id: "",
    categoryName: "",
  };
  menu: boolean = false;

  get computedDate() {
    if (this.newPurchase.buyDate) {
      return timeChangeService.formatDate(this.newPurchase.buyDate);
    }
  }

  async mounted() {
    if (this.product) {
      let category = await categoryService.getCategoryById(
        this.product.category.id
      );

      this.selected = category;

      let productTest = await productService.getProductById(
        this.selected.id,
        this.product.id
      );
      this.newPurchase.productId = productTest.id;
      this.newPurchase.product = productTest;
    }

    this.categories = await categoryService.getCategories();
  }

  async selectProducts(categoryId: string) {
    this.products = await productService.getProducts(categoryId);
  }

  close() {
    this.$emit("close");
  }

  save() {
    this.$emit("save:purchase", this.newPurchase);
  }
}
</script>

<style lang="scss" scoped>
.newPurchase {
  list-style: none;
  padding: 0;
  margin: 0;
}
</style>