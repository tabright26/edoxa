import { connect, MapDispatchToProps } from "react-redux";
import {
  LINK_GAME_CREDENTIAL_MODAL,
  UNLINK_GAME_CREDENTIAL_MODAL
} from "utils/modal/constants";
import Item from "./Item";
import { show } from "redux-modal";
import { GameOption } from "types";

interface OwnProps {}

interface DispatchProps {
  showLinkGameAccountCredentialModal: (gameOption: GameOption) => any;
  showUnlinkGameAccountCredentialModal: (gameOption: GameOption) => any;
}

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch,
  ownProps
) => {
  return {
    showLinkGameAccountCredentialModal: (gameOption: GameOption) =>
      dispatch(show(LINK_GAME_CREDENTIAL_MODAL, { gameOption })),
    showUnlinkGameAccountCredentialModal: (gameOption: GameOption) =>
      dispatch(show(UNLINK_GAME_CREDENTIAL_MODAL, { gameOption }))
  };
};

export default connect(null, mapDispatchToProps)(Item);
