<template>
  <div id="app">
    <TheMenu/>
    <TheContent/>
  </div>
</template>

<script>
import TheMenu from '@/components/layout/TheMenu'
import TheContent from '@/components/layout/TheContent'



export default {
  name: 'App',
  components: {
    TheMenu,
    TheContent
  }
}
</script>

<style>

@import url('./assets/font/fontawesome-5.15.1/css/all.css');
@import url('./css/common/main.css');
</style>
