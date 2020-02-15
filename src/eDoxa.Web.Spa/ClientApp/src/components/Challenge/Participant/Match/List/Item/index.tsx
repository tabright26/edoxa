import React, { FunctionComponent } from "react";
import { CardBody, Badge } from "reactstrap";
import Format from "components/Shared/Format";
import { compose } from "recompose";
import { MapDispatchToProps, connect } from "react-redux";
import { show } from "redux-modal";
import { CHALLENGE_MATCH_SCORE_MODAL } from "utils/modal/constants";
import Moment from "react-moment";
import { ChallengeMatch } from "types/challenges";

type OwnProps = {
  match: ChallengeMatch;
  position: number;
};

type DispatchProps = {
  showChallengeMatchScoreModal: () => void;
};

type InnerProps = DispatchProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Item: FunctionComponent<Props> = ({
  match,
  position,
  showChallengeMatchScoreModal
}) => (
  <CardBody className="p-0 border border-dark d-flex">
    <div
      className="pl-3 py-2 text-center"
      style={{
        width: "50px"
      }}
    >
      <Badge color={match.isBestOf ? "primary" : "secondary"} className="w-100">
        {position}
      </Badge>
    </div>
    <div
      className="px-3 py-2"
      style={{
        width: "350px"
      }}
    >
      <Moment unix format="lll">
        {match.gameStartedAt}
      </Moment>
    </div>
    <div
      className="py-2 text-center mx-auto"
      onClick={() => showChallengeMatchScoreModal()}
    >
      <Badge color="secondary">View details</Badge>
    </div>
    <div
      className={`${
        match.isBestOf ? "bg-primary" : "bg-secondary"
      } px-3 py-2 text-center ml-5`}
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

export default enhance(Item);
