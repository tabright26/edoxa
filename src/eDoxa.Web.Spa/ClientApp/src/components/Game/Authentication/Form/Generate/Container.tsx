import { connect } from "react-redux";
import {
  GENERATE_GAME_AUTHENTICATION_FAIL,
  GameAuthenticationActions
} from "store/actions/identity/types";
import { generateGameAuthentication } from "store/actions/identity/actions";
import Generate from "./Generate";
import { throwSubmissionError } from "utils/form/types";
import { Game } from "types";

interface OwnProps {
  game: Game;
}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    generateGameAuthentication: (data: any) =>
      dispatch(generateGameAuthentication(ownProps.game, data)).then(
        (action: GameAuthenticationActions) => {
          switch (action.type) {
            case GENERATE_GAME_AUTHENTICATION_FAIL: {
              throwSubmissionError(action.error);
              break;
            }
          }
          return Promise.resolve(action);
        }
      )
  };
};

export default connect(null, mapDispatchToProps)(Generate);
