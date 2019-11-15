import { connect } from "react-redux";
import {
  GENERATE_GAME_AUTH_FACTOR_FAIL,
  GameAuthFactorActions
} from "store/root/authFactor/types";
import { generateGameAuthFactor } from "store/root/authFactor/actions";
import Generate from "./Generate";
import { throwSubmissionError } from "utils/form/types";
import { Game } from "types";

interface OwnProps {
  game: Game;
}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    generateGameAuthFactor: (data: any) =>
      dispatch(generateGameAuthFactor(ownProps.game, data)).then(
        (action: GameAuthFactorActions) => {
          switch (action.type) {
            case GENERATE_GAME_AUTH_FACTOR_FAIL: {
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
