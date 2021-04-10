<template>
  <v-container fill-height fluid>
    <v-row align="center" justify="center">
      <!-- Validation -->
      <validation-observer ref="observer" v-slot="{ invalid }">
        <div v-if="!successful">
          <!-- Registration Form -->
          <v-form @submit.prevent="handleRegister" id="register-form">
            <!-- FirstName field -->
            <validation-provider
              v-slot="{ errors }"
              name="firstname"
              rules="required"
            >
              <v-text-field
                v-model="user.firstname"
                label="Имя пользователя"
                :error-messages="errors"
                name="firstname"
                required
              ></v-text-field>
            </validation-provider>
            <!-- End -->
            <!-- LastName field -->
            <validation-provider
              v-slot="{ errors }"
              name="lastname"
              rules="required"
            >
              <v-text-field
                v-model="user.lastname"
                label="Фамилия пользователя"
                :error-messages="errors"
                name="lastname"
                required
              ></v-text-field>
            </validation-provider>
            <!-- End -->
            <!-- Username field -->
            <validation-provider
              v-slot="{ errors }"
              name="username"
              rules="required"
            >
              <v-text-field
                v-model="user.username"
                label="Логин пользователя"
                :error-messages="errors"
                name="username"
                required
              ></v-text-field>
            </validation-provider>
            <!-- End -->
            <!-- Email field -->
            <validation-provider
              v-slot="{ errors }"
              name="email"
              rules="required"
            >
              <v-text-field
                type="email"
                v-model="user.email"
                label="Электронная почта"
                :error-messages="errors"
                name="email"
                required
              ></v-text-field>
            </validation-provider>
            <!-- End -->
            <!-- Password field -->
            <validation-provider
              v-slot="{ errors }"
              name="password"
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
            <!-- Submit register button -->
            <v-btn
              class="ma-2"
              type="submit"
              :loading="loading"
              :disabled="invalid"
              color="success"
              @click="loader = 'loading'"
              form="register-form"
            >
              Регистрация
              <template v-slot:loader>
                <span>Загрузка...</span>
              </template>
            </v-btn>
            <!-- End -->

            <!--Alert-->
            <v-alert :type="successful ? 'success' : 'error'" v-if="successful">
              {{ message }}
            </v-alert>
            <!-- End -->
          </v-form>
        </div>
        <!-- End -->
      </validation-observer>
      <!-- End -->
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { namespace } from "vuex-class";
import { required } from "vee-validate/dist/rules";
import { extend, setInteractionMode } from "vee-validate";
const Auth = namespace("Auth");

setInteractionMode("eager");
extend("required", {
  ...required,
  message: "Поле {_field_} не может быть пустым.",
});

@Component
export default class Register extends Vue {
  private user: any = { username: "", email: "", password: "" };
  private loading: boolean = false;
  private submitted: boolean = false;
  private successful: boolean = false;
  private message: string = "";

  @Auth.Getter
  private isLoggedIn!: boolean;

  @Auth.Action
  private register!: (data: any) => Promise<any>;

  mounted() {
    if (this.isLoggedIn) {
      this.$router.push("/purchases");
    }
  }

  handleRegister() {
    this.message = "";
    this.submitted = true;

    let isValid = (this.$refs.observer as Vue & {
      validate: () => boolean;
    }).validate();

    if (isValid) {
      this.register(this.user).then(
        (data) => {
          this.message = data.message;
          this.successful = true;
          this.$router.push("/login");
        },
        (error) => {
          this.message = error;
          this.successful = false;
        }
      );
    }
  }
}
</script>