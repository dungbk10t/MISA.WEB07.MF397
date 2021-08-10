<template>
    <div></div>
</template>
<script>
import { eventBus2 } from '../../main.js'
export default {
    name:"CommonFunction",
    methods: {
        validateData(data, dataType) {
        console.log(data);
        switch (dataType) {
          case "PersonName":
            //validate ten nguoi dung
            break;
          case "Email":
            //
            break;
          case "Date":
            if (data != "") {
              data += "T00:00:00";
            }
            return data;
          case "PhoneNumber":
            //
            break;
        }
        return true;
      },
    },
    created(){
        eventBus2.$on('validateData',(data)=>{
            console.log("CommonBusReceive",data)
        });
    }
}
</script>