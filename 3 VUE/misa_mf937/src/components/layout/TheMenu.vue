<template>
  <div class="menu">
    <div class="menu-header">
      <div class="toggle"></div>
      <div class="logo"></div>
    </div>
    <div class="menu-body">
      <MenuItem 
        menuItemClass="dashboard-icon" 
        menuItemText="Tổng quan" />
      <MenuItem
        menuItemClass="document-online-icon"
        menuItemText="Hồ sơ online"
      />
      <MenuItem 
        menuItemClass="report-icon" 
        menuItemText="Theo dõi chi" />
      <MenuItem
        menuItemClass="dashboard-icon"
        menuItemText="Danh mục khách hàng"
      />
      <MenuItem
        menuItemClass="dic-employee-icon"
        menuItemText="Danh mục nhân viên"
      />
      <MenuItem 
        menuItemClass="setting-icon" 
        menuItemText="Thiết lập" />
    </div>
  </div>
</template>

<script>
import MenuItem from '@/components/layout/TheMenuItem'

export default {
  name: "TheMenu",
  components: {
    MenuItem,
  },
};
</script>

<style scoped>
.menu {
    position: relative;
    width: 225px;
    height: 100vh;
    border-right: solid 1px #e4e4e4;
    float: left;
    transition: 0.5s;
    overflow: hidden;
}

    .menu .menu-header {
        width: 100%;
        height: 48px;
        display: flex;
        align-items: center;
        transition: 0.5s;
    }

        .menu .menu-header .toggle {
            background-color: #fff;
            height: 24px;
            width: 24px;
            margin: 12px 0px;
            margin-right: 10px;
            margin-left: 16px;
            background-image: url('../../assets/img/toggle.png');
            background-repeat: no-repeat;
            background-size: contain;
        }

        .menu .menu-header .logo {
            background-color: #fff;
            height: 24px;
            width: 100px;
            background-image: url('../../assets/img/cukcuk-logo.png');
            background-repeat: no-repeat;   
            background-size: contain;
            transition: 0.5s;
        }

    .menu .menu-body {
        width: 100%;
        transition: 0.5s;
    }
</style>
