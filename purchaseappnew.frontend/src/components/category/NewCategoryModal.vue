<template>
  <app-modal>
    <template v-slot:header> Добавить новую категорию </template>
    <!-- Validation -->
    <template v-slot:body>
      <validation-observer ref="observer" v-slot="{ invalid }">
        <v-container class="newCategory">
          <form @submit.prevent="submit">
            <!-- Category Name Field -->
            <validation-provider
              v-slot="{ errors }"
              name="Название категории"
              rules="required|min:7|max:20"
            >
              <v-text-field
                id="categoryName"
                label="Название категории"
                :error-messages="errors"
                v-model="newCategory.categoryName"
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
import { ICategory } from "@/types/Category";
// Validation
import Validation from "@/utils/validation-rule-service";
import CategoryRules from "@/rules/CategoryRules";
// Modals
import AppModal from "@/components/modal/AppModal.vue";

let validation = new Validation();
let rules = new CategoryRules();

validation.setRules(rules.getCategoryRules());

@Component({
  name: "NewCategoryModal",
  components: { AppModal },
})
export default class NewCategoryModal extends Vue {
  newCategory: ICategory = {
    id: "",
    categoryName: "",
  };

  close() {
    this.$emit("close");
  }

  save() {
    (this.$refs.observer as Vue & {
      validate: () => boolean;
    }).validate();
    this.$emit("save:category", this.newCategory);
  }
}
</script>

<style lang="scss" scoped>
.newCategory {
  list-style: none;
  padding: 0;
  margin: 0;
}
</style>