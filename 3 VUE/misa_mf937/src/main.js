import Vue from 'vue'
import App from './App.vue'

Vue.config.productionTip = false

export const eventBus1 = new Vue();
export const eventBus2 = new Vue();

new Vue({
  render: h => h(App),
}).$mount('#app')