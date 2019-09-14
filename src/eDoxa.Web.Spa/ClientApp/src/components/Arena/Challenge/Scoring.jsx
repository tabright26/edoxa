import React from "react";
import { Card, CardBody, CardHeader, ListGroup, ListGroupItem } from "reactstrap";

import Loading from "../../Shared/Loading";

const Body = ({ challenge }) => {
  if (!challenge) {
    return (
      <CardBody className="text-center">
        <Loading.Default />
      </CardBody>
    );
  } else {
    return (
      <ListGroup>
        {Object.entries(challenge.scoring).map((item, index) => (
          <ListGroupItem key={index} className="px-2 py-1 bg-dark">
            {item[0]}
            <span className="float-right">{item[1]}</span>
          </ListGroupItem>
        ))}
      </ListGroup>
    );
  }
};

const ArenaChallengeScoring = ({ challenge }) => {
  return (
    <Card bg="dark" className="my-2 text-light">
      <CardHeader as="h5" className="text-center">
        Scoring
      </CardHeader>
      <Body challenge={challenge} />
    </Card>
  );
};

export default ArenaChallengeScoring;
