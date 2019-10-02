import React from "react";
import { Button, Card, CardTitle, CardText, CardBody, CardHeader } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";

const ClanItem = ({ clan }) => {
  return (
    <Card className="card-accent-primary">
      <CardHeader>{clan.name}</CardHeader>
      <CardBody>
        <CardTitle>Clan Details</CardTitle>
        <CardText>
          <small className="text-muted">Clan summary: {clan.summary}</small>
        </CardText>
        <LinkContainer
          to={"/structures/clans/" + clan.id}
          style={{
            cursor: "pointer"
          }}
        >
          <Button color="primary">View Details</Button>
        </LinkContainer>
      </CardBody>
    </Card>
  );
};

export default ClanItem;
