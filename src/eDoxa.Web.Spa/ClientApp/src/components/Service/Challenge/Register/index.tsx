import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import ChallengeForm from "components/Service/Challenge/Form";
import {
  withUserProfileUserId,
  withUserProfileGameIsAuthenticated
} from "utils/oidc/containers";
import { compose } from "recompose";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { MapStateToProps, connect, DispatchProp } from "react-redux";
import { RootState } from "store/types";
import { show } from "redux-modal";
import { LINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";
import {
  HocUserProfileUserIdStateProps,
  HocUserProfileGameIsAuthenticatedStateProps
} from "utils/oidc/containers/types";
import { ChallengeId, CHALLENGE_STATE_INSCRIPTION } from "types/challenges";
import { Game } from "types/games";

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> &
  Params &
  HocUserProfileUserIdStateProps;

type StateProps = {
  readonly canRegister: boolean;
  readonly game: Game;
};

type DispatchProps = {
  showLinkGameAccountCredentialModal: () => void;
};

type InnerProps = DispatchProp &
  DispatchProps &
  OwnProps &
  StateProps &
  HocUserProfileGameIsAuthenticatedStateProps;

type OutterProps = Params & {
  readonly className?: string;
};

type Props = InnerProps & OutterProps;

const Register: FunctionComponent<Props> = ({
  className,
  userId,
  isAuthenticated,
  canRegister,
  dispatch,
  game
}) =>
  canRegister &&
  (isAuthenticated ? (
    <ChallengeForm.Register className={className} userId={userId} />
  ) : (
    <Button
      color="primary"
      className={className}
      onClick={() =>
        dispatch(
          show(LINK_GAME_CREDENTIAL_MODAL, {
            game
          })
        )
      }
    >
      Link
    </Button>
  ));

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge =>
      challenge.id ===
      (ownProps.challengeId
        ? ownProps.challengeId
        : ownProps.match.params.challengeId)
  );
  return {
    canRegister:
      challenge.state === CHALLENGE_STATE_INSCRIPTION &&
      !challenge.participants.some(
        participant => participant.userId === ownProps.userId
      ),
    game: challenge.game
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  withUserProfileUserId,
  connect(mapStateToProps),
  withUserProfileGameIsAuthenticated
);

export default enhance(Register);
