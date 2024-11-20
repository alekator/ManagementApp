import { createRouter, createWebHistory } from 'vue-router';
import Wizard from '../views/Wizard.vue';

const routes = [
  {
    path: '/',
    name: 'Wizard',
    component: Wizard
  }
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
});

export default router;
