<template>
  <div class="">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Вид услуги:"
            v-model="ad.typeItem"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Тип услуги:"
            v-model="ad.categoryItem"
          ></v-text-field>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "service-ad",
  props: ["ad"],
  data: () => ({}),
  methods: {},
};
</script>