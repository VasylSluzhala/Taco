import React from 'react';
import { Restaurant } from '../../models/restaurant';
import { Image, Row, Col, Checkbox, Form } from 'antd';
import './index.css';

interface IRestaurantItemProps {
    item: Restaurant;
    onMenuItemCheck: (isChecked: boolean, itemPrice: number) => void;
}

const RestaurantItem = ({ item, onMenuItemCheck }: IRestaurantItemProps) => {
    return (
        <Row gutter={[16, 16]} className="restaurant-item-container">
            <Col>
                <Image
                    height={100}
                    width={150}
                    preview={false}
                    src={item.logoPath}
                />
            </Col>
            <Col span={22}>
                <Row gutter={[16, 16]}>
                    <Col><h3>{`${item.name} - ${item.suburb} - rated #${item.rank} overall`}</h3></Col>
                </Row>
                <Row justify="start" gutter={[8, 8]} className="category-container">
                    {item.categories.map(cat => {
                        return (
                            <Col key={cat.name} span={24}>
                                <Row>
                                    <strong>{cat.name}</strong>
                                </Row>

                                <Row justify="start" className="menu-items-container">
                                    {cat.menuItems.map(mi => {
                                        return (
                                            <Col key={mi.id} span={24}>
                                                <Form.Item
                                                    name={mi.id}
                                                    valuePropName="checked"
                                                    className="menu-items-form-item"
                                                >
                                                    <Checkbox onChange={(event) => onMenuItemCheck(event.target.checked, mi.price)}>{mi.name} - R{mi.price}</Checkbox>
                                                </Form.Item>
                                            </Col>
                                        );
                                    })}
                                </Row>
                            </Col>
                        );
                    })}
                </Row>
            </Col>
        </Row>
    );
}

export default RestaurantItem;