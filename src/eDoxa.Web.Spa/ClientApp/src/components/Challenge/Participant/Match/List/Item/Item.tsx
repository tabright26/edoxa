import React, { FunctionComponent } from "react";
import { CardBody, Badge } from "reactstrap";
import Format from "components/Shared/Format";
import { compose } from "recompose";
import { ChallengeParticipantMatch } from "types";
import { MapDispatchToProps, connect } from "react-redux";
import { show } from "redux-modal";
import { CHALLENGE_MATCH_SCORE_MODAL } from "utils/modal/constants";

interface OwnProps {
  match: ChallengeParticipantMatch;
  position: number;
}

interface DispatchProps {
  showChallengeMatchScoreModal: () => void;
}

type InnerProps = DispatchProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const ChallengeParticipantMatchItem: FunctionComponent<Props> = ({
  match,
  position,
  showChallengeMatchScoreModal
}) => (
  <CardBody className="p-0 border border-dark d-flex">
    <div
      className="pl-2 py-2 text-center"
      style={{
        width: "45px"
      }}
    >
      <Badge variant="light">{position}</Badge>
    </div>
    {/* <div
      className="px-3 py-2"
      style={{
        width: "350px"
      }}
    >
      <Moment unix format="LLLL">
        {match.synchronizedAt}
      </Moment>
    </div> */}
    <div
      className="py-2 text-center mx-auto"
      onClick={() => showChallengeMatchScoreModal()}
    >
      <Badge variant="primary">View details</Badge>
    </div>
    <div
      className="bg-primary px-3 py-2 text-center ml-5"
      style={{
        width: "125px"
      }}
    >
      <Format.Score score={match.score} />
    </div>
  </CardBody>
);

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch,
  ownProps
) => {
  return {
    showChallengeMatchScoreModal: () =>
      dispatch(
        show(CHALLENGE_MATCH_SCORE_MODAL, { stats: ownProps.match.stats })
      )
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(null, mapDispatchToProps)
);

export default enhance(ChallengeParticipantMatchItem);
