import { connect } from "react-redux";
import { linkGameCredential } from "store/root/user/games/actions";
import { LINK_GAME_CREDENTIAL_FAIL, LinkGameCredentialAction } from "store/root/user/games/types";
import Link from "./Link";
import { throwSubmissionError } from "utils/form/types";
import { Game } from "types";

interface OwnProps {
  game: Game;
}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    linkGameCredential: () =>
      dispatch(linkGameCredential(ownProps.game)).then((action: LinkGameCredentialAction) => {
        switch (action.type) {
          case LINK_GAME_CREDENTIAL_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
        }
      })
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Link);
