import { connect, MapStateToProps } from "react-redux";
import Register from "./Register";
import { RootState } from "store/types";
import { ChallengeId, CHALLENGE_STATE_INSCRIPTION, UserId } from "types";
import { compose } from "recompose";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { withtUserProfile } from "store/root/user/container";
import { SUB_CLAIM_TYPE } from "utils/oidc/types";

interface Params {
  readonly challengeId: ChallengeId;
}

interface OwnProps extends RouteComponentProps<Params> {
  readonly userId: UserId;
}

interface StateProps {
  readonly canRegister: boolean;
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge => challenge.id === ownProps.match.params.challengeId
  );
  return {
    canRegister:
      challenge.state === CHALLENGE_STATE_INSCRIPTION &&
      !challenge.participants.some(
        participant => participant.user.id === ownProps.userId
      )
  };
};

const enhance = compose<any, any>(
  withtUserProfile(SUB_CLAIM_TYPE, "userId"),
  withRouter,
  connect(mapStateToProps)
);

export default enhance(Register);
