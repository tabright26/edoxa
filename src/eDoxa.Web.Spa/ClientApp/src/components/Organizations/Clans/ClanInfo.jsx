import React from "react";
import { Card, CardImg, CardBody, CardHeader, Row } from "reactstrap";

import { connectLogo } from "store/organizations/logos/container";

const ClanInfo = ({ clan, logo }) => {
  return (
    <Card className="card-accent-primary">
      <CardImg top width="100%" src="https://via.placeholder.com/350x150" alt="Card image cap" />
      <CardHeader>Clan Info</CardHeader>
      <CardBody>
        <Row>Name: {clan ? clan.name : ""}</Row>
        <hr />
        <Row>Summary: {clan ? clan.summary : ""}</Row>
      </CardBody>
    </Card>
  );
};

export default connectLogo(ClanInfo);
