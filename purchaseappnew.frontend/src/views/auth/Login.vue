<template>
  <v-container fill-height fluid>
    <v-row align="center" justify="center">
      <!-- Validation -->
      <validation-observer ref="observer" v-slot="{ invalid }">
        <!-- Login Form -->
        <v-form @submit.prevent="handleLogin" id="login-form">
          <!-- Username Field -->
          <validation-provider
            v-slot="{ errors }"
            name="Логин"
            rules="required"
          >
            <v-text-field
              v-model="user.username"
              label="Имя пользователя"
              :error-messages="errors"
              name="username"
              required
            ></v-text-field>
          </validation-provider>
          <!-- End -->
          <!-- Password Field -->
          <validation-provider
            v-slot="{ errors }"
            name="Пароль"
            rules="required"
          >
            <v-text-field
              type="password"
              v-model="user.password"
              label="Пароль"
              :error-messages="errors"
              name="password"
              required
            ></v-text-field>
          </validation-provider>
          <!-- End -->
          <!-- Submit login button -->
          <v-btn
            class="ma-2"
            type="submit"
            :loading="loading"
            :disabled="invalid"
            color="success"
            @click="loader = 'loading'"
            form="login-form"
          >
            Логин
            <template v-slot:loader>
              <span>Загрузка...</span>
            </template>
          </v-btn>
          <v-btn @click="resetPassword" color="primary"> Сменить пароль </v-btn>
          <!-- End -->
          <!-- Alert -->
          <v-alert type="error" v-if="message">
            {{ message }}
          </v-alert>
          <!-- End -->
        </v-form>
        <!-- End -->
      </validation-observer>
      <!-- End -->
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
// Validation
import Validation from "@/utils/validation-rule-service";
import AuthRules from "@/rules/AuthRules";

// Auth module Vuex
import { namespace } from "vuex-class";
const Auth = namespace("Auth");

// Initialization
const validation = new Validation();
const rules = new AuthRules();

// Setting validation rules
validation.setRules(rules.getAuthRules());

@Component({
  name: "Login",
  components: {},
})
export default class Login extends Vue {
  private user: any = { username: "", password: "" };
  private loading: boolean = false;
  private message: string = "";
  private loader: any = null;

  @Watch("loader")
  loaderChanged() {
    this.loading = this.loader;
    this.loading = !this.loading;

    setTimeout(() => (this.loading = false), 10000);

    this.loader = null;
  }

  @Auth.Getter
  private isLoggedIn!: boolean;

  @Auth.Action
  private login!: (data: any) => Promise<any>;

  created() {
    if (this.isLoggedIn) {
      setTimeout(() => this.$router.push("/purchases"), 3000);
    }
  }

  resetPassword() {
    this.$router.push("/reset");
  }

  handleLogin() {
    this.loading = true;
    (this.$refs.observer as Vue & { validate: () => boolean }).validate();
    if (this.user.username && this.user.password) {
      this.login(this.user).then(
        (data) => {
          this.loading = false;
          this.$router.push("/purchases");
        },
        (error) => {
          this.loading = false;
          this.message = "Ваш логин или пароль неверны.";
        }
      );
    }
  }
}
</script>
