<template>
  <div id="app">
    <TheMenu/>
    <EmployeeList/>
  </div>
</template>

<script>
import TheMenu from '@/components/layout/TheMenu'
import EmployeeList from '@/components/view/employee/EmployeeList'



export default {
  name: 'App',
  components: {
    TheMenu,
    EmployeeList
  }
}
</script>

<style>

@import url('./assets/font/fontawesome-5.15.1/css/all.css');
@import url('./css/common/main.css');
</style>
