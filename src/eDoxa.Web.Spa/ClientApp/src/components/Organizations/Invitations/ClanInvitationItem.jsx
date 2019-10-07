import React, { Fragment, useEffect, useState } from "react";
import { Col } from "reactstrap";

const ClanInvitationItem = ({ invitation, doxaTags }) => {
  const [doxaTag, setDoxaTag] = useState(null);

  useEffect(() => {
    if (doxaTags && invitation) {
      setDoxaTag(doxaTags.find(tag => tag.userId === invitation.userId));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [doxaTags]);

  return (
    <Fragment>
      <Col xs="4" sm="4" md="4">
        <small className="text-muted">{doxaTag ? doxaTag.name : ""}</small>
      </Col>
    </Fragment>
  );
};

export default ClanInvitationItem;
