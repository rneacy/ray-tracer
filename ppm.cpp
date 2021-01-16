#include "ppm.h"
#include "Colour.h"
#include "Ray.h"

int main() {
	render();
}

void render() {
	// Image rendering meta
	const auto asp_ratio = 16.0 / 9.0;
	const int i_width = 400;
	const int i_height = static_cast<int>(i_width / asp_ratio);

	// Camera meta
	auto view_height = 2.0;
	auto view_width = asp_ratio * view_height;
	auto focal_length = 1.0;

	// Worldspace meta
	auto origin = Point3();
	auto horiz = Vector3(view_width, 0, 0);
	auto vert = Vector3(0, view_height, 0);
	auto llc = origin - horiz / 2 - vert / 2 - Vector3(0, 0, focal_length);

	// P3 denotes a Portable PixMap (RGB)
	// 3, 2 is the width and height of the image in pixels
	// 0-255 are byte values for each part of RGB
	std::cout << "P3\n" << i_width << " " << i_height << "\n255\n";

	Colour white = Colour(1.0, 1.0, 1.0);
	Colour magenta = Colour(0.82, 0.2, 0.5);

	// Render
	for (int i = 0; i < i_height; i++) {
		std::cerr << "\rRendering line " << i + 1 << " of " << i_height << std::flush;

		for (int j = 0; j < i_width; j++) {
			// Send out a ray for this pixel
			auto x = double(i) / (i_width - 1);
			auto y = double(j) / (i_height - 1);

			Ray ray(origin, llc + x * horiz + y*vert - origin);
			Colour pix_col = Ray::Colour_Lerp(ray, white, magenta);
			Write_Colour(std::cout, pix_col);
		}
	}

	std::cerr << "\nRender complete.\n";
}