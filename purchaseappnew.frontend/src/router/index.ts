import Vue from "vue";
import axios from 'axios';
import VueRouter, { RouteConfig } from "vue-router";
import Login from "@/views/auth/Login.vue";
import Register from "@/views/auth/Register.vue";
import ResetPassword from "@/views/auth/ResetPassword.vue";
import ExpireDialog from "@/components/ExpireDialog.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: '/login',
    name: 'login',
    component: Login
  },
  {
    path: '/register',
    name: 'register',
    component: Register
  },
  {
    path: "/reset",
    name: 'reset',
    component: ResetPassword
  },
  {
    path: "/expire",
    name: 'expire',
    component: ExpireDialog
  },
  {
    path: '/',
    name: 'home',
    component: () => import("@/views/purchase/PurchaseList.vue")
  },
  {
    path: '/purchases',
    name: 'purchases',
    component: () => import("@/views/purchase/PurchaseList.vue")
  },
  {
    path: "/categories",
    name: 'categories',
    component: () => import("@/views/category/CategoryList.vue")
  },
  {
    path: "/products/:categoryId",
    name: 'products',
    component: () => import("@/views/product/ProductList.vue")
  },
  {
    path: "/purchases/report",
    name: 'report',
    component: () => import("@/views/report/ReportList.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

const publicRoutes = ['/login', '/register', '/reset'];

axios.interceptors.response.use((response) => {
   return response
}, error => {
    if (error.response.status == 401 && !publicRoutes) {
        localStorage.removeItem('user');
        router.push("/expire");
    }
    return Promise.reject(error);
});

export default router;
