<template>
  <div class="">
    <div v-if="isEdit === 'true'">
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
            :items="conditions"
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
    </div>
    <div v-else>
      <div class="text--primary">Статус: {{ ad.typeItem }}</div>
      <div class="text--primary">Состояние: {{ ad.condition }}</div>
      <div class="text--primary">Площадь: {{ ad.area }}</div>
      <div class="text--primary">Этаж: {{ ad.numberFloor }}</div>
      <div class="text--primary">Количество комнат: {{ ad.countRooms }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "realty",
  props: ["ad", "isEdit", "conditions"],
  data: () => ({}),
  methods: {},
};
</script>