<template>
  <v-container fluid>
    <v-alert
      v-model="isInitialized"
      type="success"
      transition="scroll-y-transition"
    >
      {{ message }}
    </v-alert>
    <h1 id="productsTitle">Cписок продуктов</h1>
    <v-row dense>
      <v-col>
        <v-btn to="/categories" color="error"> Назад </v-btn>
      </v-col>
      <v-col>
        <div class="float-right">
          <v-btn @click="showNewProductModal" color="success" id="addNewBtn">
            Добавить продукт
          </v-btn>
        </div>
      </v-col>
    </v-row>
    <div v-if="isProductsExist">
      <v-row dense>
        <v-col
          v-for="product in products"
          :key="product.id"
          :cols="product.flex"
        >
          <v-card v-if="product">
            <v-card-title
              >Название продукта: {{ product.productName }}</v-card-title
            >
            <v-card-text> Цена: {{ product.price }} </v-card-text>
            <v-card-actions>
              <v-btn color="primary" @click="showNewPurchaseModal(product)">
                Купить продукт
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </div>
    <div v-else>
      <v-row dense align="center" justify="center">
        <v-col>
          <h1 class="text-center">Нет продуктов по данной категории</h1>
        </v-col>
      </v-row>
    </div>
    <new-purchase-modal
      v-if="isNewPurchaseVisible"
      @save:purchase="saveNewPurchase"
      @close="closeModal"
      :product="selectedProduct"
    />

    <new-product-modal
      v-if="isNewProductVisible"
      @save:product="saveNewProduct"
      @close="closeModal"
    />
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
// Services
import ProductService from "@/services/product-service";
import PurchaseService from "@/services/purchase-service";

// Types
import { IProduct } from "@/types/Product";
import { IPurchase } from "@/types/Purchase";

// Modal components
import NewProductModal from "@/components/product/NewProductModal.vue";
import NewPurchaseModal from "@/components/purchase/NewPurchaseModal.vue";

// Services init
const productService = new ProductService();
const purchaseService = new PurchaseService();

@Component({
  name: "ProductList",
  components: { NewProductModal, NewPurchaseModal },
})
export default class ProductList extends Vue {
  products: IProduct[] = [];
  isProductsExist: boolean = false;
  selectedProduct: IProduct = {
    id: "",
    productName: "",
    price: 0,
    category: {
      id: "",
      categoryName: "",
    },
  };
  isNewProductVisible: boolean = false;
  isNewPurchaseVisible: boolean = false;
  categoryId: string = this.$route.params.categoryId;
  message: string = "";
  isInitialized: boolean = false;

  async initialize() {
    this.products = await productService.getProducts(this.categoryId);
    if (this.products.length > 0) {
      this.isProductsExist = true;
      this.setInitialized();
    }
  }

  async saveNewProduct(newProduct: IProduct) {
    await productService.save(this.categoryId, newProduct);
    this.isNewProductVisible = false;
    await this.initialize();
  }

  async saveNewPurchase(newPurchase: IPurchase) {
    await purchaseService.save(newPurchase);
    this.isNewPurchaseVisible = false;
    await this.initialize();
  }

  setInitialized() {
    this.message = "Отображены продукты";
    this.isInitialized = true;
  }

  closeModal() {
    this.isNewProductVisible = false;
    this.isNewPurchaseVisible = false;
  }

  showNewProductModal() {
    this.isNewProductVisible = true;
  }

  showNewPurchaseModal(product: IProduct) {
    this.isNewPurchaseVisible = true;
    this.selectedProduct = product;
  }

  async created(): Promise<void> {
    await this.initialize();
    setTimeout(() => (this.isInitialized = false), 1000);
  }
}
</script>