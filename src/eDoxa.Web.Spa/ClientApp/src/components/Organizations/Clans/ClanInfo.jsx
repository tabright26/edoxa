import React, { useEffect } from "react";
import { Card, CardImg, CardBody, CardHeader, Row } from "reactstrap";

import { connectLogo } from "store/organizations/logos/container";

const ClanInfo = ({ actions, clan, logo }) => {
  useEffect(() => {
    if (clan) {
      actions.loadLogo(clan.id);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clan]);

  return (
    <Card className="card-accent-primary">
      <CardImg top width="100%" src={logo ? logo : "https://via.placeholder.com/350x150"} alt="Card image cap" />
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
