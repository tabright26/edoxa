import React from "react";
import { Button, Card, CardTitle, CardText, CardBody, CardHeader } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";

const ClanItem = ({ clan }) => {
  return (
    <Card>
      <CardHeader>{clan.name}</CardHeader>
      <CardBody>
        <CardTitle>Clan Details</CardTitle>
        <CardText>Clan summary: {clan.summary}</CardText>
        <LinkContainer to={"/structures/clans/" + clan.id}>
          <Button color="primary">View Details</Button>
        </LinkContainer>
      </CardBody>
    </Card>
  );
};

export default ClanItem;
