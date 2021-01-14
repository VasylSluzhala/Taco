import React, { useEffect, useState } from 'react';
import './app.css';
import { Input, Form, Button, Row, Modal, message } from 'antd';
import { getRestaurantsByFilter } from "../../services/restaurant-service";
import { createOrder } from "../../services/order-service";
import { Restaurant } from '../../models/restaurant';
import RestaurantItem from '../restaurant';

const App = () => {
  const [form] = Form.useForm();
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

  const onMenuItemCheck = (isChecked: boolean, itemPrice: number) => {
    if (isChecked) {
      setTotalPrice(totalPrice + itemPrice);
    }
    else {
      setTotalPrice(totalPrice - itemPrice);
    }
  }

  const onFormFinish = (values: any) => {
    const ids = Object.entries<boolean>(values).filter(x => x[1] === true).map(x => parseInt(x[0], 10));
    createOrder(ids)
      .then(() => Modal.success({
        content: (
          <div>
            <p>You order has been placed!</p>
            <p>Leave the rest up to the chefs and our drivers!</p>
          </div>
        )
      }))
      .catch(() => { message.error("Sorry, something went wrong") });

    form.resetFields();
    setTotalPrice(0);
  }

  return (
    <div className="app">
      <Input.Search placeholder="Taco in Cape Town" onSearch={onSearch} className="search-box" />
      <Form
        form={form}
        onFinish={onFormFinish}
      >
        <div className="menu-items-main-container">
          {restaurants.map(x => <RestaurantItem onMenuItemCheck={onMenuItemCheck} item={x} key={x.id} />)}
        </div>
        <Row justify="center" align="middle" className="button-container">
          <Button htmlType="submit" type="primary" className="order-button">{`Order - R${totalPrice}`}</Button>
        </Row>
      </Form>
    </div >
  );
}

export default App;