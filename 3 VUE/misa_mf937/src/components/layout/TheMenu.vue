<template>
  <div class="menu">
    <div class="menu-header">
      <div class="toggle"></div>
      <div class="logo"></div>
    </div>
    <div class="menu-body">
      <div class="menu-item">
        <div class="menu-icon dashboard-icon"></div>
        <p>Tổng quan</p>
      </div>
      <div class="menu-item">
        <div class="menu-icon document-online-icon"></div>
        <p>Hồ sơ online</p>
      </div>
      <div class="menu-item">
        <div class="menu-icon report-icon"></div>
        <p>Theo dõi chi</p>
      </div>
      <div class="menu-item">
        <div class="menu-icon dashboard-icon"></div>
        <p>Theo dõi đóng</p>
      </div>
      <div class="menu-item">
        <div class="menu-icon dic-employee-icon"></div>
        <p>Quản lí lao động</p>
      </div>
      <div class="menu-item">
        <div class="menu-icon setting-icon"></div>
        <p>Thiết lập</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.menu {
  width: 225px;
  height: 100vh;
  border-right: solid 1px #e4e4e4;
  float: left;
}
.menu .menu-header {
  width: 100%;
  height: 48px;
  display: flex;
  align-items: center;
}

.menu .menu-header .toggle {
  background-color: #fff;
  height: 24px;
  width: 24px;
  margin: 12px 0px;
  margin-right: 10px;
  margin-left: 16px;
  background-image: url("../../assets/img/toggle.png");
  background-repeat: no-repeat;
  background-size: contain;
}

.menu .menu-header .logo {
  background-color: #fff;
  height: 24px;
  width: 100px;
  background-image: url("../../assets/img/cukcuk-logo.png");
  background-repeat: no-repeat;
  background-size: contain;
}

.menu .menu-body {
  width: 100%;
}

.menu .menu-body .menu-item {
  width: 100%;
  height: 44px;
  display: flex;
  cursor: pointer;
}

.menu .menu-body .menu-item .menu-icon {
  width: 20px;
  height: 20px;
  background-color: #fff;
  margin: auto 0;
  margin-left: 16px;
  margin-right: 10px;
  background-repeat: no-repeat;
  background-size: contain;
}

.menu .menu-body .menu-item .menu-icon.dashboard-icon {
  background-image: url("../../assets/icon/dashboard.png");
}

.menu .menu-body .menu-item .menu-icon.document-online-icon {
  background-image: url("../../assets/icon/document-online.png");
}

.menu .menu-body .menu-item .menu-icon.dic-employee-icon {
  background-image: url("../../assets/icon/dic-employee.png");
}

.menu .menu-body .menu-item .menu-icon.report-icon {
  background-image: url("../../assets/icon/report.png");
}

.menu .menu-body .menu-item .menu-icon.setting-icon {
  background-image: url("../../assets/icon/setting.png");
}

.menu .menu-body .menu-item:hover {
  background-color: #e5e5e5;
  color: #000000;
}

.menu .menu-body .menu-item:hover .menu-icon {
  background-color: #e5e5e5;
}

.menu .menu-body .menu-item p {
  font-size: 13px;
  color: #515252;
}
</style>
