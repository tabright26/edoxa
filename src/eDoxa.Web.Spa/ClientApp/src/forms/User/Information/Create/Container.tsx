import { connect } from "react-redux";
import { loadUserInformations, createUserInformations } from "store/root/user/informations/actions";
import { CREATE_USER_INFORMATIONS_FAIL, UserInformationsActions } from "store/root/user/informations/types";
import Create from "./Create";
import { throwSubmissionError } from "utils/form/types";

const mapDispatchToProps = (dispatch: any) => {
  return {
    createUserInformations: (data: any) =>
      dispatch(createUserInformations(data)).then((action: UserInformationsActions) => {
        switch (action.type) {
          case CREATE_USER_INFORMATIONS_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
          default: {
            dispatch(loadUserInformations());
            break;
          }
        }
      })
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Create);
