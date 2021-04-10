<template>
  <app-modal>
    <template v-slot:header> Добавить новый продукт </template>
    <!-- Validation -->
    <template v-slot:body>
      <validation-observer ref="observer" v-slot="{ invalid }">
        <v-container class="newProduct">
          <form @submit.prevent="submit">
            <!-- Product Name Field -->
            <validation-provider
              v-slot="{ errors }"
              name="Название продукта"
              rules="required|min:5|max:20"
            >
              <v-text-field
                id="productName"
                label="Название продукта"
                :error-messages="errors"
                v-model="newProduct.productName"
              />
            </validation-provider>
            <!-- End -->
            <!-- Product Price Field -->
            <validation-provider
              v-slot="{ errors }"
              name="Цена продукта"
              :rules="{
                required: true,
                integer: true,
                is_not: 0,
                max: 6,
                regex: '^(0|[1-9][0-9]*)$',
              }"
            >
              <v-text-field
                id="price"
                label="Цена продукта"
                :error-messages="errors"
                v-model="newProduct.price"
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
                  Добавить
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
import { Component, Vue } from "vue-property-decorator";
// Types
import { IProduct } from "@/types/Product";
// Validation
import Validation from "@/utils/validation-rule-service";
import ProductRules from "@/rules/ProductRules";
// Modals
import AppModal from "@/components/modal/AppModal.vue";

let validation = new Validation();
let rules = new ProductRules();

validation.setRules(rules.getProductRules());

@Component({
  name: "NewProductModal",
  components: { AppModal },
})
export default class NewProductModal extends Vue {
  newProduct: IProduct = {
    id: "",
    productName: "",
    price: 0,
    category: {
      id: "",
      categoryName: "",
    },
  };

  close() {
    this.$emit("close");
  }

  save() {
    (this.$refs.observer as Vue & {
      validate: () => boolean;
    }).validate();
    this.$emit("save:product", this.newProduct);
  }
}
</script>

<style lang="scss" scoped>
.newProduct {
  list-style: none;
  padding: 0;
  margin: 0;
}
</style>