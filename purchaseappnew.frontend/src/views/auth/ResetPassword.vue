<template>
  <v-container fill-height fluid>
    <v-row align="center" justify="center">
      <v-form @submit.prevent="resetUserPassword">
        <v-text-field v-model="user.username" label="Имя пользователя">
        </v-text-field>

        <v-text-field
          v-model="user.currentPassword"
          label="Текущий пароль"
          type="password"
        >
        </v-text-field>

        <v-text-field
          v-model="user.newPassword"
          label="Новый пароль"
          type="password"
        >
        </v-text-field>

        <v-text-field
          v-model="user.confirmPassword"
          label="Подтвердите новый пароль"
          type="password"
        >
        </v-text-field>

        <v-btn color="success" type="submit"> Сменить пароль </v-btn>
      </v-form>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { IUserPasswordReset } from "@/types/UserPasswordReset";
import { namespace } from "vuex-class";
const Auth = namespace("Auth");

@Component({
  name: "ResetPassword",
  components: {},
})
export default class ResetPassword extends Vue {
  user: IUserPasswordReset = {
    username: "",
    currentPassword: "",
    newPassword: "",
    confirmPassword: "",
  };
  message: string = "";

  @Auth.Action
  private resetPassword!: (data: any) => Promise<any>;

  resetUserPassword() {
    this.resetPassword(this.user).then(
      (data) => {
        this.$router.push("/login");
      },
      (error) => {
        this.message = error;
      }
    );
  }
}
</script>