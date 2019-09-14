import React from "react";
import { Card, Badge } from "react-bootstrap";
import { connect } from "react-redux";
import { show } from "redux-modal";
import Loading from "../../../../Shared/Loading";
import Format from "../../../../Shared/Format";
import Moment from "react-moment";
import { ARENA_CHALLENGE_PARTICIPANT_MATCH_SCORE_DETAILS_MODAL } from "../../../../../modals";

const Match = ({ match, position, actions }) => {
  if (!match) {
    return (
      <Card.Body className="text-center">
        <Loading.Default />
      </Card.Body>
    );
  } else {
    return (
      <Card.Body className="p-0 border border-dark d-flex">
        <div
          className="pl-2 py-2 text-center"
          style={{
            width: "45px"
          }}
        >
          <Badge variant="light">{position}</Badge>
        </div>
        <div
          className="px-3 py-2"
          style={{
            width: "350px"
          }}
        >
          <Moment unix format="LLLL">
            {match.synchronizedAt}
          </Moment>
        </div>
        <div className="py-2 text-center mx-auto" onClick={() => actions.showArenaChallengeParticipantMatchScoreDetailsModal(match.stats)}>
          <Badge variant="primary">View details</Badge>
        </div>
        <div
          className="bg-primary px-3 py-2 text-center ml-5"
          style={{
            width: "90px"
          }}
        >
          <Format.Score score={match.totalScore} />
        </div>
      </Card.Body>
    );
  }
};

const mapDispatchToProps = dispatch => {
  return {
    actions: {
      showArenaChallengeParticipantMatchScoreDetailsModal: stats => dispatch(show(ARENA_CHALLENGE_PARTICIPANT_MATCH_SCORE_DETAILS_MODAL, { stats }))
    }
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Match);
