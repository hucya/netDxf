﻿#region netDxf library, Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)

//                        netDxf library
// Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using netDxf.Blocks;
using netDxf.Tables;

namespace netDxf.Entities
{
    /// <summary>
    /// Represents a diametric dimension <see cref="EntityObject">entity</see>.
    /// </summary>
    public class DiametricDimension :
        Dimension
    {
        #region private fields

        private Vector2 center;
        private Vector2 refPoint;
        private double offset;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c>DiametricDimension</c> class.
        /// </summary>
        public DiametricDimension()
            : this(Vector2.Zero, Vector2.UnitX, 0.0, DimensionStyle.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>DiametricDimension</c> class.
        /// </summary>
        /// <param name="arc"><see cref="Arc">Arc</see> to measure.</param>
        /// <param name="rotation">Rotation in degrees of the dimension line.</param>
        /// <param name="offset">Distance between the reference point and the dimension text</param>
        /// <remarks>The center point and the definition point define the distance to be measure.</remarks>
        public DiametricDimension(Arc arc, double rotation, double offset)
            : this(arc, rotation, offset, DimensionStyle.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>DiametricDimension</c> class.
        /// </summary>
        /// <param name="arc"><see cref="Arc">Arc</see> to measure.</param>
        /// <param name="rotation">Rotation in degrees of the dimension line.</param>
        /// <param name="offset">Distance between the reference point and the dimension text</param>
        /// <param name="style">The <see cref="DimensionStyle">style</see> to use with the dimension.</param>
        /// <remarks>The center point and the definition point define the distance to be measure.</remarks>
        public DiametricDimension(Arc arc, double rotation, double offset, DimensionStyle style)
            : base(DimensionType.Diameter)
        {
            if (arc == null)
                throw new ArgumentNullException(nameof(arc));

            Vector3 ocsCenter = MathHelper.Transform(arc.Center, arc.Normal, CoordinateSystem.World, CoordinateSystem.Object);
            this.center = new Vector2(ocsCenter.X, ocsCenter.Y);
            this.refPoint = Vector2.Polar(this.center, arc.Radius, rotation*MathHelper.DegToRad);
            this.offset = offset;

            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
            this.Normal = arc.Normal;
            this.Elevation = ocsCenter.Z;
        }

        /// <summary>
        /// Initializes a new instance of the <c>DiametricDimension</c> class.
        /// </summary>
        /// <param name="circle"><see cref="Circle">Circle</see> to measure.</param>
        /// <param name="rotation">Rotation in degrees of the dimension line.</param>
        /// <param name="offset">Distance between the reference point and the dimension text</param>
        /// <remarks>The center point and the definition point define the distance to be measure.</remarks>
        public DiametricDimension(Circle circle, double rotation, double offset)
            : this(circle, rotation, offset, DimensionStyle.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>DiametricDimension</c> class.
        /// </summary>
        /// <param name="circle"><see cref="Circle">Circle</see> to measure.</param>
        /// <param name="rotation">Rotation in degrees of the dimension line.</param>
        /// <param name="offset">Distance between the reference point and the dimension text</param>
        /// <param name="style">The <see cref="DimensionStyle">style</see> to use with the dimension.</param>
        /// <remarks>The center point and the definition point define the distance to be measure.</remarks>
        public DiametricDimension(Circle circle, double rotation, double offset, DimensionStyle style)
            : base(DimensionType.Diameter)
        {
            if (circle == null)
                throw new ArgumentNullException(nameof(circle));

            Vector3 ocsCenter = MathHelper.Transform(circle.Center, circle.Normal, CoordinateSystem.World, CoordinateSystem.Object);
            this.center = new Vector2(ocsCenter.X, ocsCenter.Y);
            this.refPoint = Vector2.Polar(this.center, circle.Radius, rotation*MathHelper.DegToRad);
            if (offset < 0.0)
                throw new ArgumentOutOfRangeException(nameof(offset), "The offset value cannot be negative.");
            this.offset = offset;

            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
            this.Normal = circle.Normal;
            this.Elevation = ocsCenter.Z;
        }

        /// <summary>
        /// Initializes a new instance of the <c>DiametricDimension</c> class.
        /// </summary>
        /// <param name="centerPoint">Center <see cref="Vector2">point</see> of the circumference.</param>
        /// <param name="referencePoint"><see cref="Vector2">Point</see> on circle or arc.</param>
        /// <param name="offset">Distance between the reference point and the dimension text</param>
        /// <remarks>The center point and the definition point define the distance to be measure.</remarks>
        public DiametricDimension(Vector2 centerPoint, Vector2 referencePoint, double offset)
            : this(centerPoint, referencePoint, offset, DimensionStyle.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <c>DiametricDimension</c> class.
        /// </summary>
        /// <param name="centerPoint">Center <see cref="Vector2">point</see> of the circumference.</param>
        /// <param name="referencePoint"><see cref="Vector2">Point</see> on circle or arc.</param>
        /// <param name="offset">Distance between the reference point and the dimension text</param>
        /// <param name="style">The <see cref="DimensionStyle">style</see> to use with the dimension.</param>
        /// <remarks>The center point and the definition point define the distance to be measure.</remarks>
        public DiametricDimension(Vector2 centerPoint, Vector2 referencePoint, double offset, DimensionStyle style)
            : base(DimensionType.Diameter)
        {
            this.center = centerPoint;
            this.refPoint = referencePoint;

            if (offset < 0.0)
                throw new ArgumentOutOfRangeException(nameof(offset), "The offset value cannot be negative.");
            this.offset = offset;

            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the center <see cref="Vector2">point</see> of the circumference in OCS (object coordinate system).
        /// </summary>
        public Vector2 CenterPoint
        {
            get { return this.center; }
            set { this.center = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="Vector2">point</see> on circumference or arc in OCS (object coordinate system).
        /// </summary>
        public Vector2 ReferencePoint
        {
            get { return this.refPoint; }
            set { this.refPoint = value; }
        }

        /// <summary>
        /// Gets or sets the distance between the center point and the dimension text.
        /// </summary>
        /// <remarks>
        /// If the value is close to half of the diameter, the offset will be 2*DIMASZ+DIMGAP*DIMSCALE.
        /// If the value is set to zero the dimension will be shown at the center, both arrowheads will be shown and no center mark will be drawn.
        /// </remarks>
        public double Offset
        {
            get { return this.offset; }
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException(nameof(value), "The offset value cannot be negative.");
                this.offset = value;
            }
        }

        /// <summary>
        /// Actual measurement.
        /// </summary>
        public override double Measurement
        {
            get { return 2*Vector2.Distance(this.center, this.refPoint); }
        }

        #endregion

        #region public methods

        /// <summary>
        /// Calculates the reference point and dimension offset from a point along the dimension line.
        /// </summary>
        /// <param name="point">Point along the dimension line.</param>
        public void SetDimensionLinePosition(Vector2 point)
        {
            double radius = Vector2.Distance(this.center, this.refPoint);
            double rotation = Vector2.Angle(this.center, point);
            this.refPoint = Vector2.Polar(this.center, radius, rotation);
            this.offset = Vector2.Distance(this.center, point);
        }

        #endregion

        #region overrides

        /// <summary>
        /// Gets the block that contains the entities that make up the dimension picture.
        /// </summary>
        /// <param name="name">Name to be assigned to the generated block.</param>
        /// <returns>The block that represents the actual dimension.</returns>
        internal override Block BuildBlock(string name)
        {
            return DimensionBlock.Build(this, name);
        }

        /// <summary>
        /// Creates a new DiametricDimension that is a copy of the current instance.
        /// </summary>
        /// <returns>A new DiametricDimension that is a copy of this instance.</returns>
        public override object Clone()
        {
            DiametricDimension entity = new DiametricDimension
            {
                //EntityObject properties
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.IsVisible,
                //Dimension properties
                Style = (DimensionStyle) this.Style.Clone(),
                AttachmentPoint = this.AttachmentPoint,
                LineSpacingStyle = this.LineSpacingStyle,
                LineSpacingFactor = this.LineSpacingFactor,
                UserText = this.UserText,
                //DiametricDimension properties
                CenterPoint = this.center,
                ReferencePoint = this.refPoint,
                Offset = this.offset,
                Elevation = this.Elevation
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}