import React from "react";
import { Button, Card, CardTitle, CardText, CardBody, CardHeader, Badge } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";
import Moment from "react-moment";

const ChallengeCard = ({ challenge }) => {
  return (
    <Card className="card-accent-primary">
      <CardHeader>
        {challenge.name}
        <div className="card-header-actions">
          <Badge pill color="danger" className="float-right">
            {challenge.state}
          </Badge>
        </div>
      </CardHeader>
      <CardBody>
        <CardTitle>Challenge Details</CardTitle>
        <CardText>This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</CardText>
        <CardText>
          <small className="text-muted">
            <Moment unix format="ll">
              {challenge.timeline.createdAt}
            </Moment>
          </small>
        </CardText>
        <LinkContainer
          to={"/arena/challenges/" + challenge.id}
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

export default ChallengeCard;
