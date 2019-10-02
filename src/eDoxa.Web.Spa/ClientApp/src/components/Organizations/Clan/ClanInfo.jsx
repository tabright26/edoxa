import React from "react";
import { Card, CardImg, CardBody, CardHeader, Row } from "reactstrap";

const ClanInfo = ({ info }) => {
  return (
    <Card className="card-accent-primary">
      <CardImg top width="100%" src="https://via.placeholder.com/350x150" alt="Card image cap" />
      <CardHeader>Clan Info</CardHeader>
      <CardBody>
        <Row>Name: {info.nane}</Row>
        <hr />
        <Row>Summary: {info.summary}</Row>
      </CardBody>
    </Card>
  );
};

export default ClanInfo;
