import React from "react";
import { Card } from "react-bootstrap";

import Loading from "../../../../../../components/Loading";

import Match from "./Details";

const Matches = ({ participant }) => {
  if (!participant) {
    return (
      <Card.Body className="text-center">
        <Loading.Default />
      </Card.Body>
    );
  } else {
    return (
      <>
        {participant.matches
          .sort((left, right) => (left.totalScore < right.totalScore ? 1 : -1))
          .map((match, index) => (
            <Match key={index} match={match} position={index + 1} />
          ))}
      </>
    );
  }
};

export default Matches;
