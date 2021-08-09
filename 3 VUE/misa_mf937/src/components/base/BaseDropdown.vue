<template>
  <div
    v-bind:class="['dropdown form-dropdown drop-up',dropdownClass]"
    v-bind:dropdownId="dropdownId"
  >
    <div class="dropdown-list" 
      :class="[{'active': dropdownActive}]"
    >
      <div 
        v-bind:class="['dropdown-list-item',dropdownItemList, {'dropdown-item-check' : item[dropdownItemValueId]==currentId}]"
        v-for="(item,index) in itemlist" :key="index" 
        @click="[clickItem(item[dropdownItemValueId], item[dropdownItemValueName]),
          dropdownOnClick()
        ]"
      >
    <i class="fas fa-check" 
      v-bind:class="[{'checked' : item[dropdownItemValueId]==currentId}]"
    >
    </i>
    <div class="dd-item-text">{{item[dropdownItemValueName]}}</div>
  </div>
    </div>
    <div class="dropdown-select" @click="dropdownOnClick">
      <div class="select" > {{currentName}}</div>
      <i class="fas fa-angle-down"></i>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "BaseDropdown",
  data() {
    return {
      itemlist: [],
      currentId: "-1",
      currentName: "",
      // opened: false,
      // defaultId: "-1",
      dropdownActive: false,
    };
  },
  props: {
    dropdownClass: String,
    dropdownId: String,
    dropdownItemList: String,
    dropdownItemValueId: String,
    dropdownItemValueName: String,
    tabindex: String,
    selectedId: String,
    myUrl: String,
    defaultName: String,
  },
  created() {
    this.loadDropdownData();
    this.currentName = this.defaultName;
    this.initChoice();
  },
  watch: {
    selectedId: function () {
      this.initChoice();
      console.log(this.selectedId);
    },
  },
  methods: {
    loadDropdownData() {
      switch (this.dropdownItemValueName) {
        case "GenderName":
          this.itemlist = [
            {
              Gender: 0,
              GenderName: "Nữ",
            },
            {
              Gender: 1,
              GenderName: "Nam",
            },
            {
              Gender: 2,
              GenderName: "Không xác định",
            },
          ];
          break;
        case "WorkStatusName":
          this.itemlist = [
            {
              WorkStatus: 0,
              WorkStatusName: "Đang thử việc",
            },
            {
              WorkStatus: 1,
              WorkStatusName: "Đang làm việc",
            },
            {
              WorkStatus: 2,
              WorkStatusName: "Đang nghỉ phép",
            },
            {
              WorkStatus: 3,
              WorkStatusName: "Đã nghỉ việc",
            },
          ];
          break;
        case "StoreName":
          this.itemlist = [
            {
              StoreId: 0,
              StoreName: "Nhà hàng Biển Đông",
            },
            {
              StoreId: 1,
              StoreName: "Nhà hàng Phú Quốc",
            },
            {
              StoreId: 2,
              StoreName: "Nhà hàng Đà Nẵng",
            },
            {
              StoreId: 3,
              StoreName: "Nhà hàng Hà Nội",
            },
          ];
          break;
        default:
          if (this.myUrl) {
            axios
              .get(`http://cukcuk.manhnv.net/${this.myUrl}`)
              .then((res) => {
                this.itemlist = res.data;
                console.log(this.myUrl);
              })
              .catch((error) => {
                console.log(error);
              });
          }
          break;
      }
    },
    initChoice() {
      console.log("SelectedID : "+this.selectedId)
      let vm = this;
      if ((vm.selectedId + "").length > 0) {
        vm.itemlist.forEach((item) => {
          if (vm.selectedId == item[vm.dropdownItemValueId]) {
            vm.currentName = item[vm.dropdownItemValueName];
            vm.currentId = item[vm.dropdownItemValueId];
          }
        });
      } else {
        vm.currentId = -1;
        vm.currentName = "";
      }
    },
    clickItem(itemValueId, dropdownItemValueName) {
      this.currentId = itemValueId;
      this.currentName = dropdownItemValueName;
      this.opened = false;
      this.$emit("input", itemValueId);
    },
    
    dropdownOnClick() {
      this.dropdownActive = !this.dropdownActive;
    }
  },
};
</script>

<style scoped>
</style>