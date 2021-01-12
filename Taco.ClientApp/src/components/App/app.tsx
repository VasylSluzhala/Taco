import React, { useEffect, useState } from 'react';
import './app.css';
import { Input, Form, Button } from 'antd';
import { getRestaurantsByFilter } from "../../services/restaurant-service";
import { Restaurant } from '../../models/restaurant';
import RestaurantItem from '../restaurant';

const App = () => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[]);
  const [totalPrice, setTotalPrice] = useState(0);

  useEffect(() => {
    getRestaurantsByFilter({ keyword: "", location: "" }).then((response: Restaurant[]) => {
      setRestaurants(response);
    });
  }, []);

  const onSearch = (value: string | undefined) => {
    if (value) {
      const splited = value.split("in");
      getRestaurantsByFilter({ keyword: splited[0].trim(), location: splited[1].trim() }).then((response: Restaurant[]) => {
        setRestaurants(response);
      });
    }
    else {
      getRestaurantsByFilter({ keyword: "", location: "" }).then((response: Restaurant[]) => {
        setRestaurants(response);
      });
    }
  }

  return (
    <div className="app">
      <Input.Search placeholder="Taco in Cape Town" onSearch={onSearch} className="search-box" />
      <Form>
        {restaurants.map(x => <RestaurantItem item={x} key={x.id} />)}
        <Button>{`Order - ${totalPrice}`}</Button>
      </Form>
    </div>
  );
}

export default App;