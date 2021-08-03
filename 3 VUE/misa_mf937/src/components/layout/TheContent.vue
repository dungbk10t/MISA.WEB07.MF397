<template>
  <div class="content">
    <EmployeeList/>
  </div>
</template>

<script>
import EmployeeList from '@/components/view/employee/EmployeeList';

export default {
  name: 'TheContent',
  components: { 
    EmployeeList,
  }
}
</script>

<style>

</style>
