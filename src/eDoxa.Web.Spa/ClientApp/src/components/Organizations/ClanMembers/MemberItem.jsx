import React, { Fragment } from "react";
import { toastr } from "react-redux-toastr";
import { Col, Button } from "reactstrap";

const MemberItem = ({ member, actions, isOwner }) => {
  return (
    <Fragment>
      <Col>{member.userDoxaTag}</Col>
      {isOwner ? (
        <Col>
          <Button
            color="warning"
            onClick={() =>
              actions
                .kickMember(member.clanId, member.memberId)
                .then(toastr.success("SUCCESS", "Member was kicked in the butt."))
                .catch(toastr.error("WARNINGAVERTISSEMENTAVECLELOGODUFBIQUIDECOLEPUAVANTLEFILM", "Member was not kicked in the butt."))
            }
          >
            Kick the butt
          </Button>
        </Col>
      ) : null}
    </Fragment>
  );
};

export default MemberItem;
