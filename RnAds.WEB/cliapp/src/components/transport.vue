<template>
  <div class="">
    <div v-if="isEdit === 'true'">
      <v-row>
        <v-col>
          <v-slider
            v-model="ad.yearReliase"
            type="number"
            label="Год выпуска"
            :thumb-color="color"
            thumb-label="always"
            max="2022"
            min="1950"
          ></v-slider>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            type="number"
            label="Либо введите год выпуска:"
            v-model="ad.yearReliase"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            label="Пробег, км"
            v-model="ad.kilometers"
            type="number"
            filled
            rounded
            dense
          ></v-text-field>
        </v-col>
      </v-row>
      <v-text-field
        label="Количество владельцев:"
        type="number"
        v-model="ad.ownerCount"
        filled
        rounded
        dense
      ></v-text-field>
      <v-row>
        <v-col>
          <v-slider
            v-model="ad.engineVolume"
            label="Обьем двигателя:"
            type="number"
            :thumb-color="color"
            thumb-label="always"
            max="5"
            min="0"
            step="0.1"
          ></v-slider>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            type="number"
            label="Либо введите обьем двигателя:"
            v-model="ad.engineVolume"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="л.с:"
            type="number"
            v-model="ad.horsePower"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-radio-group label="Тип двигателя: " v-model="ad.typeEngine" row>
            <v-radio label="Бензин" value="Бензин"></v-radio>
            <v-radio label="Дизель" value="Дизель"></v-radio>
            <v-radio label="Гибрид" value="Гибрид"></v-radio>
            <v-radio label="Электро" value="Электро"></v-radio>
          </v-radio-group>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-radio-group label="Коробка передач: " v-model="ad.typeGearBox" row>
            <v-radio label="Автомат" value="Автомат"></v-radio>
            <v-radio label="Механика" value="Механика"></v-radio>
          </v-radio-group>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-radio-group label="Привод:" v-model="ad.typeDrive" row>
            <v-radio label="Задний" value="Задний"></v-radio>
            <v-radio label="Передний" value="Передний"></v-radio>
            <v-radio label="Полный" value="Полный"></v-radio>
          </v-radio-group>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-radio-group label="Тип кузова:" v-model="ad.typeBody" row>
            <v-radio label="Купе" value="Купе"></v-radio>
            <v-radio label="Универсал" value="Универсал"></v-radio>
            <v-radio label="Седан" value="Седан"></v-radio>
            <v-radio label="Пикап" value="Пикап"></v-radio>
            <v-radio label="Хэтчбэк" value="Хэтчбэк"></v-radio>
          </v-radio-group>
        </v-col>
      </v-row>
      <v-text-field
        label="Цвет:"
        v-model="ad.color"
        filled
        rounded
        dense
      ></v-text-field>
      <v-row>
        <v-col>
          <v-radio-group label="Руль:" v-model="ad.typeHelm" row>
            <v-radio label="Правый" value="Правый"></v-radio>
            <v-radio label="Левый" value="Левый"></v-radio>
          </v-radio-group>
        </v-col>
      </v-row>

    </div>
    <div v-else>
      <div class="text--primary">Год выпуска: {{ ad.yearReliase }}</div>
      <div class="text--primary">Пробег: {{ ad.kilometers }}</div>
      <div class="text--primary">
        Количество владельцев: {{ ad.ownerCount }}
      </div>
      <div class="text--primary">Обьем двигателя: {{ ad.engineVolume }}</div>
      <div class="text--primary">л.с: {{ ad.horsePower }}</div>
      <div class="text--primary">Тип двигателя: {{ ad.typeEngine }}</div>
      <div class="text--primary">Коробка передач: {{ ad.typeGearBox }}</div>
      <div class="text--primary">Привод: {{ ad.typeDrive }}</div>
      <div class="text--primary">Тип кузова: {{ ad.typeBody }}</div>
      <div class="text--primary">Цвет: {{ ad.color }}</div>
      <div class="text--primary">Руль: {{ ad.typeHelm }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "transport",
  props: ["ad","isEdit"],
  data: () => ({
    color: "red"
  }),
  methods: {},
};
</script>