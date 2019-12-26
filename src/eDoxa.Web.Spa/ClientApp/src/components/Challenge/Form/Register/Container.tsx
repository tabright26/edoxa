import { connect } from "react-redux";
import {
  loadChallenge,
  registerChallengeParticipant
} from "store/actions/challenge";
import {
  ChallengesActions,
  REGISTER_CHALLENGE_PARTICIPANT_SUCCESS,
  REGISTER_CHALLENGE_PARTICIPANT_FAIL
} from "store/actions/challenge/types";
import Register from "./Register";
import { ChallengeId } from "types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { toastr } from "react-redux-toastr";
import { compose } from "recompose";

interface Params {
  readonly challengeId?: ChallengeId;
}

interface OwnProps extends RouteComponentProps<Params> {}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    registerChallengeParticipant: () =>
      dispatch(
        registerChallengeParticipant(ownProps.match.params.challengeId)
      ).then((action: ChallengesActions) => {
        switch (action.type) {
          case REGISTER_CHALLENGE_PARTICIPANT_SUCCESS: {
            return loadChallenge(ownProps.match.params.challengeId);
          }
          case REGISTER_CHALLENGE_PARTICIPANT_FAIL: {
            return toastr.error(
              "ERROR",
              "An error occurred while registering as a challenge participant."
            );
          }
          default: {
            return Promise.resolve();
          }
        }
      })
  };
};

const enhance = compose<any, any>(
  withRouter,
  connect(null, mapDispatchToProps)
);

export default enhance(Register);
