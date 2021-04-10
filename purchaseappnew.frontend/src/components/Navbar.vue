<template>
  <nav>
    <!-- Navigation Bar -->
    <v-app-bar app clipped-left dense color="deep-purple accent-4">
      <v-app-bar-nav-icon
        class="white--text"
        @click.stop="drawer = !drawer"
      ></v-app-bar-nav-icon>
      <v-toolbar-title class="text-uppercase" @click="goToRoute('/')">
        <span class="white--text">Приложение покупок</span>
      </v-toolbar-title>

      <v-spacer></v-spacer>
      <!-- If logged in -->
      <div v-if="currentUser" class="container-fluid">
        <v-btn text color="white">
          <span>Добро пожаловать, {{ currentUser.userName }} !</span>
        </v-btn>

        <v-btn text color="white" @click="logout">
          <span>Выход</span>
          <v-icon>exit_to_app</v-icon>
        </v-btn>
      </div>
      <!-- End -->
      <!-- If not logged in -->
      <div v-if="!currentUser" class="container-fluid">
        <v-btn text color="white" @click="goToRoute('/login')">
          <span>Логин</span>
        </v-btn>

        <v-btn text color="white" @click="goToRoute('/register')">
          <span>Регистрация</span>
        </v-btn>
      </div>
      <!-- End -->
    </v-app-bar>
    <!-- End -->

    <!-- Navigation Drawer -->
    <v-navigation-drawer
      v-model="drawer"
      app
      color="white"
      clipped
      style="padding-top: 65px"
    >
      <v-list nav dense>
        <v-list-item-group
          v-if="currentUser"
          v-model="group"
          class="text-uppercase"
        >
          <v-list-item @click="goToRoute('/categories')">
            <v-list-item-title>Категории</v-list-item-title>
          </v-list-item>
          <v-list-item @click="goToRoute('/purchases')">
            <v-list-item-title>Покупки</v-list-item-title>
          </v-list-item>
          <v-list-item @click="goToRoute('/purchases/report')">
            <v-list-item-title>Расходы по категориям</v-list-item-title>
          </v-list-item>
        </v-list-item-group>
        <v-list-item-group
          v-if="!currentUser"
          v-model="group"
          class="text-uppercase"
        >
          <v-list-item @click="goToRoute('/login')">
            <v-list-item-title>Логин</v-list-item-title>
          </v-list-item>
          <v-list-item @click="goToRoute('/register')">
            <v-list-item-title>Регистрация</v-list-item-title>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
    <!-- End -->
  </nav>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { namespace } from "vuex-class";
const Auth = namespace("Auth");
@Component({
  name: "Navbar",
  components: {},
})
export default class Navbar extends Vue {
  drawer: boolean = false;
  group: Array<Object>[] = [];

  @Watch("group")
  setGroup() {
    this.drawer = false;
  }

  @Auth.State("user")
  private currentUser!: any;

  @Auth.Action
  private signOut!: () => void;

  goToRoute(route: string) {
    if (this.$route.path !== route) this.$router.push(route);
  }

  logout() {
    this.signOut();
    this.$router.push("/login");
  }
}
</script>