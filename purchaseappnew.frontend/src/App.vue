<template>
  <v-app>
    <navbar />
    <v-main> <router-view /> </v-main>
  </v-app>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Navbar from "@/components/Navbar.vue";
import { namespace } from "vuex-class";
const Auth = namespace("Auth");

@Component({
  name: "App",
  components: { Navbar },
})
export default class App extends Vue {
  @Auth.State("user")
  private currentUser!: any;

  mounted() {
    if (!this.currentUser) {
      this.$router.push("/login");
    }
  }
}
</script>

