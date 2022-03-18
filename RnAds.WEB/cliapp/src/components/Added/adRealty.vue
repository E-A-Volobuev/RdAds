<template>
  <div class="">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Статус:"
            v-model="ad.typeItem"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-combobox
            v-model="ad.сondition"
            label="Состояние:"
            outlined
            dense
          ></v-combobox>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
         <v-text-field
            dense
            type="number"
            label="Площадь:"
            v-model="ad.area"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
         <v-text-field
            dense
            type="number"
            label="Этаж:"
            v-model="ad.numberFloor"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
         <v-text-field
            dense
            type="number"
            label="Количество комнат:"
            v-model="ad.countRooms"
          ></v-text-field>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "realty-ad",
  props: ["ad","conditions"],
  data: () => ({}),
  methods: {},
};
</script>