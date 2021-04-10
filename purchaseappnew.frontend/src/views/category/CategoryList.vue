<template>
  <v-container fluid>
    <h1>Категории</h1>
    <v-row dense>
      <v-col>
        <v-btn @click="showNewCategoryModal" color="success" id="addNewBtn"
          >Добавить категорию</v-btn
        >
      </v-col>
    </v-row>
    <v-row dense>
      <v-col
        v-for="category in categories"
        :key="category.id"
        :cols="category.flex"
      >
        <v-card>
          <v-card-title>{{ category.categoryName }}</v-card-title>
          <v-card-actions class="justify-center">
            <v-btn
              @click="goToProduct(category.id)"
              align="center"
              color="primary"
            >
              Список продуктов
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>

    <new-category-modal
      v-if="isNewCategoryVisible"
      @save:category="saveNewCategory"
      @close="closeModal"
    />
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
// Services
import CategoryService from "@/services/category-service";

// Types
import { ICategory } from "@/types/Category";

// Modal components
import NewCategoryModal from "@/components/category/NewCategoryModal.vue";

// Services init
const categoryService = new CategoryService();

@Component({
  name: "CategoryList",
  components: { NewCategoryModal },
})
export default class CategoryList extends Vue {
  categories: ICategory[] = [];
  isNewCategoryVisible: boolean = false;

  async initialize() {
    this.categories = await categoryService.getCategories();
  }

  async saveNewCategory(newCategory: ICategory) {
    await categoryService.save(newCategory);
    this.isNewCategoryVisible = false;
    await this.initialize();
  }

  closeModal() {
    this.isNewCategoryVisible = false;
  }

  showNewCategoryModal() {
    this.isNewCategoryVisible = true;
  }

  async created() {
    await this.initialize();
  }

  goToProduct(categoryId: string) {
    this.$router.push({ name: "products", params: { categoryId } });
  }
}
</script>