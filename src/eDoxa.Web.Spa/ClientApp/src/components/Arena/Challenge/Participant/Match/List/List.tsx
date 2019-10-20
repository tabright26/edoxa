import React from "react";
import { CardBody } from "reactstrap";
import Loading from "components/Shared/Loading";
import Match from "components/Arena/Challenge/Participant/Match/Details/Details";

const Matches = ({ participant }) => {
  if (!participant) {
    return (
      <CardBody className="text-center">
        <Loading />
      </CardBody>
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
