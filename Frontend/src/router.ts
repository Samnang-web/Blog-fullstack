import { createRouter, createWebHistory } from "vue-router";
import Home from "./Pages/Home.vue";
import Dashboard from "./Pages/admin/Dashboard.vue";
import AddBlog from "./Pages/admin/AddBlog.vue";
import Users from "./Pages/admin/Users.vue";
import Update from "./Pages/admin/update.vue";
import Login from "./admin/Login.vue";
import BlogPages from "./components/BlogPages.vue";
import ListBlog from "./Pages/admin/ListBlog.vue";
import Layout from "./Pages/admin/Layout.vue";
import Profile from "./Pages/admin/Profile.vue";
import Category from "./Pages/admin/Category.vue";

const routes = [
  { path: "/", name: "Home", component: Home },
  { path: "/login", name: "Login", component: Login },
  { path: "/blogs/:id", name: "BlogPages", component: BlogPages },
  {
    path: "/admin",
    name: "Layout",
    component: Layout,
    meta: { requiresAuth: true },
    children: [
      {
        path: "",
        name: "Dashboard",
        component: Dashboard,
        
      },
      {
        path: "profile",
        name: "Profile",
        component: Profile,
        meta: { requiresAuth: true },
      },
      {
        path: "addblog",
        name: "AddBlog",
        component: AddBlog,
        meta: { requiresAuth: true },
      },
      {
        path: "listblog",
        name: "ListBlog",
        component: ListBlog,
        meta: { requiresAuth: true },
      },
      {
        path: "category",
        name: "Category",
        component: Category,
        meta: { requiresAuth: true },
      },
      {
        path: "users",
        name: "Users",
        component: Users,
        meta: { requiresAuth: true },
      },
      {
        path: "edit/:id",
        name: "EditBlog",
        component: Update,
        meta: { requiresAuth: true },
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, _, next) => {
  const token = localStorage.getItem("token");
  const requiresAuth = to.matched.some((record) => record.meta.requiresAuth);

  console.log(
    "[Guard]",
    to.fullPath,
    "| requiresAuth:",
    requiresAuth,
    "| token:",
    token
  );

  if (requiresAuth && !token) {
    next("/login?redirect=" + to.fullPath);
  } else {
    next();
  }
});

export default router;
