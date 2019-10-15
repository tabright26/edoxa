import React from "react";
import { CardBody, Badge } from "reactstrap";
import { connect } from "react-redux";
import { show } from "redux-modal";
import Loading from "components/Shared/Override/Loading";
import Format from "components/Shared/Format";
import Moment from "react-moment";
import { MATCH_SCORE_MODAL } from "modals";

const Match = ({ match, position, actions }) => {
  if (!match) {
    return (
      <CardBody className="text-center">
        <Loading />
      </CardBody>
    );
  } else {
    return (
      <CardBody className="p-0 border border-dark d-flex">
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
      </CardBody>
    );
  }
};

const mapDispatchToProps = dispatch => {
  return {
    actions: {
      showArenaChallengeParticipantMatchScoreDetailsModal: stats => dispatch(show(MATCH_SCORE_MODAL, { stats }))
    }
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Match);
