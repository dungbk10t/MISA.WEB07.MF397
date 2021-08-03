<template>
  <div class="content-body">
    <ContentTitle/>
    <ContentToolbar/>
    <Table/>
    <ContentPaging/>
  </div>
</template>

<script>
import ContentTitle from "./TheContentTitle.vue"
import ContentToolbar from "./TheContentToolbar.vue"
import ContentPaging from "./TheContentPaging.vue"
import Table from './TheTable.vue';

export default {
    name:'TheContentBody',
    components:{
      ContentTitle, ContentToolbar, Table, ContentPaging,
    }
};
</script>

<style scoped>
.content .content-body {
  width: 100%;
  height: calc(100% - 49px);
  float: left;
  padding: 24px;
  box-sizing: border-box;
}
</style>