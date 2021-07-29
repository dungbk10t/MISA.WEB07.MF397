<template>
  <div class="content">
    <TheHeader/>
    <TheBody/>
  </div>
</template>

<script>

import TheHeader from '@/components/layout/TheHeader'
import TheBody from '@/components/layout/TheBody'

export default {
  name: 'TheContent',
  components: {
    TheHeader,
    TheBody,
  }
}
</script>

<style>

</style>
