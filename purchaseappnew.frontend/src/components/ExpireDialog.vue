<template>
  <v-dialog width="500" transition="dialog-top-transition" v-model="dialog">
    <v-card>
      <v-card-title class="headline grey lighten-2">
        Предупреждение
      </v-card-title>

      <v-card-text>
        Ваша сессия закончилась. Перемещаем на страницу авторизации.
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="error" text @click="redirect()"> Хорошо </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component({
  name: "ExpireDialog",
  components: {},
})
export default class ExpireDialog extends Vue {
  dialog: boolean = false;

  created() {
    this.dialog = true;
  }

  redirect(): void {
    this.dialog = false;

    let router = this.$router;
    setTimeout(function () {
      router.push("/login");
      location.reload();
    }, 100);
  }
}
</script>
