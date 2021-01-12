import React from 'react';
import { Restaurant } from '../../models/restaurant';
import { Avatar, Row, Col, Checkbox } from 'antd';
import './index.css';

const RestaurantItem = ({ item }: { item: Restaurant }) => {
    return (
        <Row gutter={[16, 16]} className="restaurant-item-container">
            <Col>
                <Avatar shape="square">{"IMG"}</Avatar>
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
                                                <Checkbox value={mi.id} >{mi.name} - R{mi.price}</Checkbox>
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